import java.sql.*;

public class Main {
    public static void main(String[] args) {
        try {
            Class.forName("org.gjt.mm.mysql.Driver");
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        }
        try (Connection connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/lab", "root", "1111")) {
            PreparedStatement statement = connection.prepareStatement("select w.*,q.title,q.description from worker w " +
                    "inner join qualification q on w.qualid=q.qualid  order BY workerID");
            ResultSet rs = statement.executeQuery();
            System.out.println("Before changes: ");
            printTable(rs);
            PreparedStatement stat;
            try {
                stat = connection.prepareStatement("update worker set qualID=? where workerID=?");
                int i = 0;
                stat.setInt(++i, 1);
                stat.setInt(++i, 2);
                if (stat.executeUpdate() > 0) {
                    System.out.println("Record was updated");
                }
                rs = statement.executeQuery();
                System.out.println("After update: ");
                printTable(rs);
            } catch (SQLException e) {
                e.printStackTrace();
            }

            try {
                stat = connection.prepareStatement("insert into worker values(?,?,?,?,?)");
                int i = 0;
                stat.setInt(++i, 4);
                stat.setString(++i, "Kolivanov");
                stat.setString(++i, "Ivan");
                stat.setDate(++i, Date.valueOf("1985-12-01"));
                stat.setInt(++i, 1);
                if (stat.executeUpdate() > 0) {
                    System.out.println("Record was added");
                }
                rs = statement.executeQuery();
                System.out.println("After insert: ");
                printTable(rs);
            } catch (SQLException e) {
                e.printStackTrace();
            }

            try {
                stat = connection.prepareStatement("delete from worker where workerid = ?");
                stat.setInt(1, 4);
                if (stat.executeUpdate() > 0) {
                    System.out.println("Record was deleted");
                }
                rs = statement.executeQuery();
                System.out.println("After delete: ");
                printTable(rs);
            } catch (SQLException e) {
                e.printStackTrace();
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    private static void printTable(ResultSet rs) throws SQLException {
        while (rs.next()) {
            System.out.println(rs.getInt("WorkerID") + "   " + rs.getString("Surname") + "   "
                    + rs.getString("Name") + "   " + rs.getDate("BDay") + "   "
                    + rs.getString("Title") + "   " + rs.getString("Description"));
        }
    }
}
