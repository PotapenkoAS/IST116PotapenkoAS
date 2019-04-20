package ru.vlsu.lab5.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.context.WebApplicationContext;
import org.springframework.web.servlet.ModelAndView;
import ru.vlsu.lab5.bean.Worker;
import ru.vlsu.lab5.service.MainService;

import java.util.ArrayList;

@Controller
@Scope(value = WebApplicationContext.SCOPE_SESSION)
public class MainController {

    private final MainService mainService;

    @Autowired
    public MainController(MainService mainService) {
        this.mainService = mainService;
    }

    @GetMapping("/")
    public String getIndex(@RequestParam(name = "text", required = false, defaultValue = "") String text, Model model) {

        ArrayList<Worker> workers = mainService.getAllWorkers();
        model.addAttribute("workers", workers);
        model.addAttribute("history", mainService.getHistory());
        model.addAttribute("text", text);
        model.addAttribute("message", mainService.getMessage());
        return "index";
    }

    @GetMapping(value = "/add")
    public String getAdd() {
        return "add";
    }

    @PostMapping(value = "/add")
    public ModelAndView postAdd(Worker worker) {
        String text = (mainService.createWorker(worker)) ? "Success" : "Fail";
        mainService.addHistory(worker);
        return new ModelAndView("redirect:/?text=" + text);
    }


    @GetMapping(value = "/up/{id}")
    public String getUpdate(@PathVariable(value = "id") int id, Model model) {
        Worker worker = mainService.getWorkerById(id);
        model.addAttribute("worker", worker);
        return "update";
    }

    @PostMapping(value = "up/{id}")
    public ModelAndView postUpdate(@PathVariable(value = "id") int id, Worker worker) {
        worker.setWorkerID(id);
        mainService.updateWorker(worker);
        mainService.addHistory(worker);
        return new ModelAndView("redirect:/");
    }

}

