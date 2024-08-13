package com.sawwere.titlecounter.frontend.app.config;

import jakarta.servlet.FilterChain;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import lombok.RequiredArgsConstructor;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Component;
import org.springframework.web.filter.OncePerRequestFilter;

import java.io.IOException;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

@Component
@RequiredArgsConstructor
public class AuthenticationFilter extends OncePerRequestFilter {
    private final List<String> publicEndpoints = List.of(
            "/login");
    @Override
    protected void doFilterInternal(HttpServletRequest request, HttpServletResponse response, FilterChain filterChain)
            throws ServletException, IOException {

        if (publicEndpoints.stream().anyMatch(x -> x.equals(request.getServletPath()))) {
            filterChain.doFilter(request, response);
        } else{
            String username = request.getHeader("username");
            String authorities = request.getHeader("authorities");
            if(username != null && authorities != null) {
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
                }
                catch (Exception e) {
                    response.sendError(401, e.getMessage());
                }
            } else {
                filterChain.doFilter(request, response);
            }
        }
    }
}
