import java.io.*;
import java.net.*;

public class Client {
    public static void main(String[] args) {
        try {
            Socket socket = new Socket("localhost", 1234);
            PrintWriter out = new PrintWriter(socket.getOutputStream(), true);
            BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()));

            // Nhập username
            BufferedReader consoleReader = new BufferedReader(new InputStreamReader(System.in));
            System.out.print("Enter your username: ");
            String username = consoleReader.readLine();
            out.println(username);

            // Nhận tin nhắn từ server
            ServerMessageHandler messageHandler = new ServerMessageHandler(in);
            messageHandler.start();

            // Gửi tin nhắn từ client tới server
            String message;
            while (true) {
                message = consoleReader.readLine();
                out.println(message);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}

class ServerMessageHandler extends Thread {
    private BufferedReader in;

    public ServerMessageHandler(BufferedReader in) {
        this.in = in;
    }

    public void run() {
        try {
            String serverMessage;
            while ((serverMessage = in.readLine()) != null) {
                System.out.println(serverMessage);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}