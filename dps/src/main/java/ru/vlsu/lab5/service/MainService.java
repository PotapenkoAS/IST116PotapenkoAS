package ru.vlsu.lab5.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Service;
import org.springframework.web.context.WebApplicationContext;
import ru.vlsu.lab5.bean.Worker;
import ru.vlsu.lab5.service.interfaces.IConn;
import ru.vlsu.lab5.service.interfaces.IHistory;
import ru.vlsu.lab5.service.interfaces.ISingle;

import javax.ejb.EJB;
import java.sql.*;
import java.util.ArrayList;

@Service
@Scope(value = WebApplicationContext.SCOPE_SESSION)
public class MainService {

    @EJB
    private IConn iConn;
    @EJB
    private ISingle iSingle;
    @EJB
    private IHistory iHistory;



    public ArrayList getHistory(){
        return iHistory.getHistory();
    }

    public void addHistory(Worker worker){
        iHistory.addHistory(worker);
    }

    public ArrayList<Worker> getAllWorkers() {
        ArrayList<Worker> result = new ArrayList<>();
        try {
            Connection conn = iConn.getConn();
            Statement statement = conn.createStatement();
            String sql = " select * from lab.worker;";

            ResultSet rs = statement.executeQuery(sql);
            iConn.closeConn(conn);
            while (rs.next()) {
                result.add(new Worker(rs.getInt(1), rs.getString(2), rs.getString(3), rs.getInt(4)));
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return result;
    }

    public Worker getWorkerById(int id) {
        Worker worker = null;
        try {
            Connection conn = iConn.getConn();
            PreparedStatement statement = conn.prepareStatement("select * from lab.worker where WorkerID=?");
            statement.setInt(1, id);
            ResultSet rs = statement.executeQuery();
            iConn.closeConn(conn);
            while (rs.next())
                worker = new Worker(rs.getInt(1), rs.getString(2), rs.getString(3), rs.getInt(4));
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return worker;
    }

    public boolean createWorker(Worker worker) {
        boolean flag = false;
        try {
            Connection conn = iConn.getConn();
            PreparedStatement statement = conn.prepareStatement("insert into lab.worker values(?,?,?,?)");
            int i = 1;
            statement.setInt(i++, worker.getWorkerID());
            statement.setString(i++, worker.getSurname());
            statement.setString(i++, worker.getName());
            statement.setInt(i, worker.getQualID());
            flag = (statement.executeUpdate() > 0);
            iConn.closeConn(conn);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return flag;
    }

    public void updateWorker(Worker worker) {
        try {
            Connection conn = iConn.getConn();
            PreparedStatement statement = conn.prepareStatement("update lab.worker set Name = ?,Surname=?,QualID=? where WorkerID=?");
            int i = 1;
            statement.setString(i++, worker.getName());
            statement.setString(i++, worker.getSurname());
            statement.setInt(i++, worker.getQualID());
            statement.setInt(i, worker.getWorkerID());
            statement.executeUpdate();
            iConn.closeConn(conn);
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }


    public String getMessage() {
        return iSingle.getMessage();
    }
}


