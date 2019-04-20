package ru.vlsu.lab5.service.impl;

import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;
import org.springframework.web.context.WebApplicationContext;
import ru.vlsu.lab5.bean.Worker;
import ru.vlsu.lab5.service.interfaces.IHistory;

import javax.annotation.PostConstruct;
import javax.annotation.PreDestroy;
import javax.ejb.Stateful;
import java.util.ArrayList;

@Stateful
@Component
@Scope(value = WebApplicationContext.SCOPE_SESSION)
public class History implements IHistory {
    private ArrayList<Worker> history;

    @Override
    public ArrayList<Worker> getHistory() {
        return history;
    }

    @Override
    public void addHistory(Worker worker) {
        history.add(worker);
    }

    @PostConstruct
    public void initHistory() {
        history = new ArrayList<>();
        System.out.println("History initialized !");
    }
    @PreDestroy
    public void preDestroyHistory(){
        history = null;
        System.out.println("History destroyed");
    }
}
