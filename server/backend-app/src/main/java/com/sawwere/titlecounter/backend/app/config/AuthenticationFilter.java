package com.sawwere.titlecounter.backend.app.config;

import jakarta.servlet.FilterChain;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.stream.Collectors;
import java.util.stream.Stream;
import lombok.RequiredArgsConstructor;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Component;
import org.springframework.web.filter.OncePerRequestFilter;



@Component
@RequiredArgsConstructor
public class AuthenticationFilter extends OncePerRequestFilter {
    @Override
    protected void doFilterInternal(HttpServletRequest request, HttpServletResponse response, FilterChain filterChain)
            throws ServletException, IOException {
        String username = request.getHeader("username");
        String authorities = request.getHeader("authorities");
        if (username != null && authorities != null) {
            try {
                UsernamePasswordAuthenticationToken authenticationToken =
                        new UsernamePasswordAuthenticationToken(
                                username,
                                null,
                                Stream.of(authorities.split(" "))
                                        .map(SimpleGrantedAuthority::new)
                                        .collect(Collectors.toList()));
                SecurityContextHolder.getContext().setAuthentication(authenticationToken);
                filterChain.doFilter(request, response);
            } catch (Exception e) {
                response.sendError(401, e.getMessage());
            }
        } else {
            filterChain.doFilter(request, response);
        }
    }
}
