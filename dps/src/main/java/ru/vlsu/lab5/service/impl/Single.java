package ru.vlsu.lab5.service.impl;

import org.springframework.stereotype.Component;
import ru.vlsu.lab5.service.interfaces.ISingle;

import javax.annotation.PostConstruct;
import javax.ejb.Singleton;

@Singleton
@Component
public class Single implements ISingle {
    private String message = "Initializer doesn't work";

    public String getMessage() {
        return message;
    }

    @PostConstruct
    public void initSingle(){
        message="Hello, World !!!";
    }
}
