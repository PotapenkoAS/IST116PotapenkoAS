package ru.vlsu.lab5.service.interfaces;

import ru.vlsu.lab5.bean.Worker;

import javax.ejb.Remote;
import java.util.ArrayList;
@Remote
public interface IHistory {
    ArrayList<Worker> getHistory();

    void addHistory(Worker worker);
}
