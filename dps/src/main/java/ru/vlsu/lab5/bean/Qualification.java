package ru.vlsu.lab5.bean;


public class Qualification {

    private int qualID;
    private String title;
    private String description;

    public int getQualID() {
        return qualID;
    }

    public Qualification setQualID(int qualID) {
        this.qualID = qualID;
        return this;
    }

    public String getTitle() {
        return title;
    }

    public Qualification setTitle(String title) {
        this.title = title;
        return this;
    }

    public String getDescription() {
        return description;
    }

    public Qualification setDescription(String description) {
        this.description = description;
        return this;
    }
}
