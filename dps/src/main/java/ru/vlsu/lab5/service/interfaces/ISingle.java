package ru.vlsu.lab5.service.interfaces;

import javax.ejb.Remote;

@Remote
public interface ISingle {
    String getMessage();
}
