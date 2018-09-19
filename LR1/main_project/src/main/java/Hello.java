import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;

public class Hello {
    private static String sayHello() {
        return "Hello ";
    }

    public static void main(String[] args) throws FileNotFoundException {
        World world = new World();
        File file = new File("main_project/src/main/resources/file.txt");
        Scanner sc = new Scanner(file);
        Helper helper = new Helper(",everyone");
        System.out.println(sayHello() + world.sayWorld() + helper.getValue() + sc.next());
    }
}
