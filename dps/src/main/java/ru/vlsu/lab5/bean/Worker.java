package ru.vlsu.lab5.bean;

public class Worker {
    private int workerID;
    private String surname;
    private String name;
    private int qualID;

    public Worker(int workerID, String surname, String name, int qualID) {
        this.workerID = workerID;
        this.name = name;
        this.surname = surname;
        this.qualID = qualID;
    }

    public int getWorkerID() {
        return workerID;
    }

    public void setWorkerID(int workerID) {
        this.workerID = workerID;
    }

    public String getSurname() {
        return surname;
    }

    public void setSurname(String surname) {
        this.surname = surname;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getQualID() {
        return qualID;
    }

    public void setQualID(int qualID) {
        this.qualID = qualID;
    }

    @Override
    public String toString() {
        return workerID + "  " + name + "  " + surname + "  " + qualID;
    }
}
