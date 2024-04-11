//package com.TitleCounter.AuthorizationServer.service;
//
//import com.TitleCounter.AuthorizationServer.storage.entity.User;
//import com.TitleCounter.AuthorizationServer.storage.repository.UserRepository;
//import lombok.RequiredArgsConstructor;
//import org.springframework.security.core.userdetails.UserDetails;
//import org.springframework.security.core.userdetails.UserDetailsService;
//import org.springframework.security.core.userdetails.UsernameNotFoundException;
//import org.springframework.stereotype.Service;
//
//@Service
//@RequiredArgsConstructor
//public class UserService implements UserDetailsService {
//
//    private final UserRepository userRepository;
//
//    @Override
//    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
//        User entity = userRepository.findByEmail(username);
//        if (entity == null) {
//            throw new UsernameNotFoundException("User with username = " + username + " not found");
//        }
//        return entity;
//    }
//}
