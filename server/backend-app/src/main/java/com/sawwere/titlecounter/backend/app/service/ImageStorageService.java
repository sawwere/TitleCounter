package com.sawwere.titlecounter.backend.app.service;

import com.sawwere.titlecounter.backend.app.config.FileStorageConfig;
import com.sawwere.titlecounter.backend.app.exception.NotFoundException;
import com.sawwere.titlecounter.backend.app.exception.StorageException;
import java.io.IOException;
import java.io.InputStream;
import java.net.MalformedURLException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.StandardCopyOption;
import java.util.stream.Stream;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.io.Resource;
import org.springframework.core.io.UrlResource;
import org.springframework.stereotype.Service;
import org.springframework.util.FileSystemUtils;
import org.springframework.web.multipart.MultipartFile;

@Service
public class ImageStorageService implements StorageService {
    private static final String EMPTY_FILE_EXCEPTION_MESSAGE = "Failed to store empty file";
    private static final String STORE_FILE_EXCEPTION_MESSAGE = "Failed to store file";
    private static final String FILE_NAME_TEMPLATE = "%s.jpg";

    private final Path rootLocation;

    @Autowired
    public ImageStorageService(FileStorageConfig properties) {

        if (properties.getBaseLocation().trim().isEmpty()) {
            throw new StorageException("File upload location can not be Empty.");
        }
        this.rootLocation = Paths.get(properties.getImageLocation());
    }

    @Override
    public void init() {
        try {
            Files.createDirectories(rootLocation);
        } catch (IOException e) {
            throw new StorageException("Could not initialize storage", e);
        }
    }

    @Override
    public void store(MultipartFile file, String filename) {
        try {
            if (file.isEmpty()) {
                throw new StorageException(EMPTY_FILE_EXCEPTION_MESSAGE);
            }
            Path destinationFile = this.rootLocation.resolve(FILE_NAME_TEMPLATE.formatted(filename))
                    .normalize().toAbsolutePath();
            try (InputStream inputStream = file.getInputStream()) {
                Files.copy(inputStream, destinationFile,
                        StandardCopyOption.REPLACE_EXISTING);
            }
        } catch (IOException e) {
            throw new StorageException(STORE_FILE_EXCEPTION_MESSAGE, e);
        }
    }

    public void store(byte[] file, String filename) {
        try {
            if (file.length  < 1) {
                throw new StorageException(EMPTY_FILE_EXCEPTION_MESSAGE);
            }
            Path destinationFile = this.rootLocation.resolve(FILE_NAME_TEMPLATE.formatted(filename))
                    .normalize().toAbsolutePath();
            Files.write(destinationFile, file);
        } catch (IOException e) {
            throw new StorageException(STORE_FILE_EXCEPTION_MESSAGE, e);
        }
    }

    @Override
    public Stream<Path> loadAll() {
        try {
            return Files.walk(this.rootLocation, 1)
                    .filter(path -> !path.equals(this.rootLocation))
                    .map(this.rootLocation::relativize);
        } catch (IOException e) {
            throw new StorageException("Failed to read stored files", e);
        }
    }

    @Override
    public Path load(String filename) {
        return rootLocation.resolve(filename);
    }

    @Override
    public Resource loadAsResource(String filename) {
        try {
            Path file = load(filename);
            Resource resource = new UrlResource(file.toUri());
            if (resource.exists() || resource.isReadable()) {
                return resource;
            } else {
                throw new NotFoundException(
                        "Resource not found: " + filename);
            }
        } catch (MalformedURLException e) {
            throw new RuntimeException("Could not read file: " + filename, e);
        }
    }

    @Override
    public void deleteAll() {
        FileSystemUtils.deleteRecursively(rootLocation.toFile());
    }
}
