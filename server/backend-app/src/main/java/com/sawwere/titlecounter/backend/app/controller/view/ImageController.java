package com.sawwere.titlecounter.backend.app.controller.view;

import com.sawwere.titlecounter.backend.app.service.ImageStorageService;
import lombok.RequiredArgsConstructor;
import org.springframework.core.io.Resource;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.ResponseBody;

@Controller
@RequiredArgsConstructor
public class ImageController {
    public static final String GET = "/api/images/{subDirectory}/{filename}";

    private final ImageStorageService imageStorageService;

    @GetMapping(value = GET, produces = MediaType.IMAGE_JPEG_VALUE)
    public @ResponseBody Resource get(
            @PathVariable(value = "subDirectory") String subDirectory,
            @PathVariable(value = "filename") String filename) {
        return imageStorageService.loadAsResource(subDirectory + "/" + filename);
    }
}
