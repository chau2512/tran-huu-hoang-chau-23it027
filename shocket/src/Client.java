import java.io.*;
import java.net.*;

public class Client {
    private static Socket socket;
    private static BufferedReader in;
    private static PrintWriter out;
    private static BufferedReader consoleReader;

    public static void main(String[] args) {
        try {
            socket = new Socket("localhost", 1234);
            in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            out = new PrintWriter(socket.getOutputStream(), true);

            // Khởi tạo luồng đọc từ bàn phím của client
            consoleReader = new BufferedReader(new InputStreamReader(System.in));
            System.out.println("Enter messages to send to the server:");

            // Khởi động luồng để nhận tin nhắn từ server
            ServerMessageHandler serverMessageHandler = new ServerMessageHandler();
            serverMessageHandler.start();

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

    static class ServerMessageHandler extends Thread {
        public void run() {
            try {
                String serverMessage;
                while ((serverMessage = in.readLine()) != null) {
                    System.out.println("Message from server: " + serverMessage);
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
}