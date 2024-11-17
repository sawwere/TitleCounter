package com.sawwere.titlecounter.backend.app.service;

import com.sawwere.titlecounter.backend.app.dto.JwtAuthenticationResponse;
import com.sawwere.titlecounter.backend.app.dto.user.UserDtoFactory;
import com.sawwere.titlecounter.backend.app.dto.user.UserRegistrationDto;
import com.sawwere.titlecounter.backend.app.exception.AlreadyExistsException;
import com.sawwere.titlecounter.backend.app.exception.ApiBadCredentialsException;
import com.sawwere.titlecounter.backend.app.exception.NotFoundException;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import com.sawwere.titlecounter.backend.app.util.TimeBasedCache;
import com.sawwere.titlecounter.common.dto.event.UserRegisteredEventDto;
import com.sawwere.titlecounter.common.dto.user.UserLoginDto;
import jakarta.servlet.http.HttpServletRequest;
import java.util.Locale;
import java.util.UUID;
import java.util.logging.Logger;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.AuthenticationException;
import org.springframework.stereotype.Service;
import static com.sawwere.titlecounter.backend.app.controller.api.AuthController.API_LOGIN;
import static org.springframework.http.HttpHeaders.AUTHORIZATION;


@Service
@RequiredArgsConstructor
public class AuthService {
    public static final String REGISTER_CONFIRM = "/api/auth/register/confirm?token=%s";
    private static final Logger LOGGER =
            Logger.getLogger(AuthService.class.getName());
    private static final String AUTH_HEADER_PREFIX = "Bearer ";

    private final AuthenticationManager authenticationManager;

    private final JwtService jwtService;
    private final UserService userService;

    @Autowired(required = false)
    private RabbitProducerService rabbitProducerService;

    private final UserDtoFactory userDtoFactory;

    @Value("${app.publicUrl}")
    private String appUrl;
    @Value("${token.accessExpirationTimeout}")
    private int accessExpirationTimeout;

    private final TimeBasedCache<String, Long> userRegistrationTokens = new TimeBasedCache<>(24 * 60 * 60 * 1000);

    /**
     * Performs login operation for user
     * @param userLoginDto user dto containing required data
     * @return pair jwt access and refresh tokens on successful login
     * @throws AuthenticationException on authentication failure
     * @throws NotFoundException in case there is no user with given credentials
     */
    @SuppressWarnings("checkstyle:MagicNumber")
    public JwtAuthenticationResponse login(UserLoginDto userLoginDto) throws
            AuthenticationException, NotFoundException {
        authenticationManager.authenticate(new UsernamePasswordAuthenticationToken(
                userLoginDto.getUsername(),
                userLoginDto.getPassword()
        ));

        try {
            var user = userService
                    .findUserByUsername(userLoginDto.getUsername());
            String accessToken = jwtService.createAccessToken(user, API_LOGIN);
            String refreshToken = jwtService.createRefreshToken(user.getUsername());
            return new JwtAuthenticationResponse(accessToken, refreshToken, accessExpirationTimeout * 60);
        } catch (NotFoundException ex) {
            throw new ApiBadCredentialsException("Invalid credentials given");
        }
    }

    /**
     * Registers new user
     * @param userRegistrationDto the dto object storing user info
     * @throws AlreadyExistsException in case there already is user with given credentials
     */
    public void register(UserRegistrationDto userRegistrationDto) throws AlreadyExistsException {
        User user = userService.createUser(userRegistrationDto);
        if (rabbitProducerService != null) {
            sendConfirmationMessage(user);
        }
        LOGGER.info("Registered user with id %d".formatted(user.getId()));
    }

    /**
     * Resends message containing request for the account confirmation link
     * @param userRegistrationDto user dto containing information about user requesting resend
     * @throws AlreadyExistsException in case user has already been confirmed
     * @throws ApiBadCredentialsException in case there is no pending confirmation requests for user
     */
    public void resendRegister(UserRegistrationDto userRegistrationDto) throws
            AlreadyExistsException, ApiBadCredentialsException {
        try {
            User user = userService.findUserByUsername(userRegistrationDto.getUsername());
            if (user.isEnabled()) {
                throw new AlreadyExistsException("Account has already been confirmed");
            } else {
                sendConfirmationMessage(user);
            }
        } catch (NotFoundException notFoundException) {
            throw new ApiBadCredentialsException("User with given username was not found");
        }
        LOGGER.info("Resent confirmation link for user with id %s".formatted(userRegistrationDto.getUsername()));
    }

    /**
     * Publishes message about user's registration
     * @param user the user to whom message needs to be delivered
     */
    public void sendConfirmationMessage(User user) {
        String token = UUID.randomUUID().toString();
        String actualLink = appUrl + REGISTER_CONFIRM.formatted(token);
        rabbitProducerService.send(UserRegisteredEventDto.builder()
                .user(userDtoFactory.entityToDto(user))
                .confirmationLink(actualLink)
                .locale(Locale.ENGLISH)
                .build()
        );
        userRegistrationTokens.put(token, user.getId());
    }

    /**
     * Confirms user's registration
     * @param token unique string identifier
     */
    public void confirmRegister(String token) {
        Long userId = userRegistrationTokens.get(token);
        if (userId == null) {
            throw new NotFoundException("Confirmation link was not found");
        }
        userService.enableUser(userId);
        userRegistrationTokens.remove(token);
        LOGGER.info("Registration confirmed for user with id %d".formatted(userId));
    }

    /**
     * Returns new access token based on refresh token in request
     * @param request request containing refresh token
     * @return pair of jwt access and refresh tokens
     */
    public JwtAuthenticationResponse refreshToken(HttpServletRequest request) {
        String authorizationHeader = request.getHeader(AUTHORIZATION);
        if (authorizationHeader == null || !authorizationHeader.startsWith(AUTH_HEADER_PREFIX)) {
            throw new RuntimeException("Token is missing");
        }

        String refreshToken = request.getHeader(AUTHORIZATION).substring(AUTH_HEADER_PREFIX.length());
        try {
            UsernamePasswordAuthenticationToken authenticationToken = jwtService.parseToken(refreshToken);
            User user = userService.findUserByUsername(authenticationToken.getName());
            String accessToken = jwtService.createAccessToken(
                    user,
                    request.getRequestURL().toString()
            );
            return new JwtAuthenticationResponse(accessToken, refreshToken, accessExpirationTimeout);
        } catch (Exception e) {
            throw new ApiBadCredentialsException("Invalid jwt was given");
        }

    }
}
