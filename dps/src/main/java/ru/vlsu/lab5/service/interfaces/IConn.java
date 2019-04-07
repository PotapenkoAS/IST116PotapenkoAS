package ru.vlsu.lab5.service.interfaces;

import javax.ejb.Local;
import java.sql.Connection;

@Local
public interface IConn {
    Connection getConn();

    void closeConn(Connection conn);
}
