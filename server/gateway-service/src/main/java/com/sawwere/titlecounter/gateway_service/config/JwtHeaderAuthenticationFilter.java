package com.sawwere.titlecounter.gateway_service.config;

import com.nimbusds.jose.JOSEException;
import com.nimbusds.jose.JWSAlgorithm;
import com.nimbusds.jose.crypto.MACVerifier;
import com.nimbusds.jose.jwk.source.ImmutableSecret;
import com.nimbusds.jose.proc.BadJOSEException;
import com.nimbusds.jose.proc.JWSKeySelector;
import com.nimbusds.jose.proc.JWSVerificationKeySelector;
import com.nimbusds.jose.proc.SecurityContext;
import com.nimbusds.jwt.JWTClaimsSet;
import com.nimbusds.jwt.SignedJWT;
import com.nimbusds.jwt.proc.ConfigurableJWTProcessor;
import com.nimbusds.jwt.proc.DefaultJWTProcessor;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.cloud.gateway.filter.GatewayFilterChain;
import org.springframework.cloud.gateway.filter.GlobalFilter;
import org.springframework.http.HttpStatus;
import org.springframework.http.server.reactive.ServerHttpRequest;
import org.springframework.http.server.reactive.ServerHttpResponse;
import org.springframework.stereotype.Component;
import org.springframework.web.server.ServerWebExchange;
import reactor.core.publisher.Mono;

import java.text.ParseException;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Logger;

import static org.springframework.http.HttpHeaders.AUTHORIZATION;

@Component
@RequiredArgsConstructor
public class JwtHeaderAuthenticationFilter implements GlobalFilter {
    private static final Logger logger = Logger.getLogger(JwtHeaderAuthenticationFilter.class.getName());
    public static final String BEARER_PREFIX = "Bearer ";
    @Value("${token.signing.key}")
    private String jwtSigningKey;

    private final List<String> publicEndpoints = List.of(
            "/auth/login", "/api/auth/login", "/auth/register", "/api/auth/register", "/api/auth/token");
    @Override
    public Mono<Void> filter(ServerWebExchange exchange, GatewayFilterChain chain) {
        ServerHttpRequest request = (ServerHttpRequest) exchange.getRequest();
        if (publicEndpoints.stream().anyMatch(x -> x.equals(request.getPath().toString()))) {
            chain.filter(exchange);
        } else {
            var list = request.getHeaders().getOrEmpty(AUTHORIZATION);
            if (list.isEmpty() || !list.get(0).startsWith(BEARER_PREFIX)) {
                ServerHttpResponse response = exchange.getResponse();
                response.setStatusCode(HttpStatus.UNAUTHORIZED);
                return response.setComplete();
            }
            String token = list.get(0).substring(BEARER_PREFIX.length());
            try {
                var parseResult = parseToken(token);
                exchange.getRequest().mutate()
                        .header("username", parseResult.username())
                        .header("authorities", String.join(" ", parseResult.authorities()))
                        .build();
            }
            catch (BadJOSEException e) {
                ServerHttpResponse response = exchange.getResponse();
                response.setStatusCode(HttpStatus.UNAUTHORIZED);
                return response.setComplete();
            }
            catch (JOSEException | ParseException e) {
                ServerHttpResponse response = exchange.getResponse();
                response.setStatusCode(HttpStatus.BAD_REQUEST);
                return response.setComplete();
            }
        }
        return chain.filter(exchange);
    }

    private TokenParseResult parseToken(String token) throws JOSEException, ParseException,
            BadJOSEException {
        byte[] secretKey = jwtSigningKey.getBytes();
        SignedJWT signedJWT = SignedJWT.parse(token);
        signedJWT.verify(new MACVerifier(secretKey));
        ConfigurableJWTProcessor<SecurityContext> jwtProcessor = new DefaultJWTProcessor<>();

        JWSKeySelector<SecurityContext> keySelector = new JWSVerificationKeySelector<>(JWSAlgorithm.HS256,
                new ImmutableSecret<>(secretKey));
        jwtProcessor.setJWSKeySelector(keySelector);
        jwtProcessor.process(signedJWT, null);
        JWTClaimsSet claims = signedJWT.getJWTClaimsSet();
        String username = claims.getSubject();
        var roles = (List<String>) claims.getClaim("roles");
        return new TokenParseResult(username, roles);
    }

    private record TokenParseResult(String username, List<String> authorities){

    }
}
