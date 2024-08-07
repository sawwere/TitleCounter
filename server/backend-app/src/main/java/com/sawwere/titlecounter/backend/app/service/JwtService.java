package com.sawwere.titlecounter.backend.app.service;

import com.nimbusds.jose.*;
import com.nimbusds.jose.crypto.MACSigner;
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
import com.sawwere.titlecounter.backend.app.storage.entity.Role;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.stereotype.Service;

import java.text.ParseException;
import java.time.Instant;
import java.util.Date;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class JwtService {
    @Value("${token.accessExpirationTimeout}")
    private int accessExpirationTimeout;
    @Value("${token.refreshExpirationTimeout}")
    private int refreshExpirationTimeout;

    @Value("${token.signing.key}")
    private String jwtSigningKey;

    public String generateToken(Payload payload) {
        try {
            JWSObject jwsObject = new JWSObject(new JWSHeader(JWSAlgorithm.HS256), payload);

            jwsObject.sign(new MACSigner(jwtSigningKey));
            return jwsObject.serialize();
        }
        catch (JOSEException e) {
            throw new RuntimeException("Error to create JWT", e);
        }
    }

    public String createAccessToken(User user, String issuer) {
        JWTClaimsSet claims = new JWTClaimsSet.Builder()
                    .subject(user.getUsername())
                    .issuer(issuer)
                    .claim("roles", user.getRoles().stream().map(Role::getName).toList())
                    .claim("email", user.getEmail())
                    .expirationTime(Date.from(Instant.now().plusSeconds(accessExpirationTimeout * 60L)))
                    .issueTime(new Date())
                    .build();

        Payload payload = new Payload(claims.toJSONObject());
        return generateToken(payload);
    }

    public String createRefreshToken(String username) {
        JWTClaimsSet claims = new JWTClaimsSet.Builder()
                .subject(username)
                .expirationTime(Date.from(Instant.now().plusSeconds(refreshExpirationTimeout * 3600L)))
                .build();

        Payload payload = new Payload(claims.toJSONObject());
        return generateToken(payload);
    }

    public UsernamePasswordAuthenticationToken parseToken(String token) throws JOSEException, ParseException,
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
        var authorities = roles == null ? null : roles.stream()
                .map(SimpleGrantedAuthority::new)
                .collect(Collectors.toList());
        return new UsernamePasswordAuthenticationToken(username, null, authorities);
    }
}
