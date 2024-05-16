import java.io.*;
import java.net.*;

public class Server {
    private static Socket clientSocket;
    private static BufferedReader in;
    private static PrintWriter out;
    private static BufferedReader consoleReader;

    public static void main(String[] args) {
        try {
            ServerSocket serverSocket = new ServerSocket(1234);
            System.out.println("Server started. Waiting for client...");

            clientSocket = serverSocket.accept();
            System.out.println("Client connected: " + clientSocket);

            in = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
            out = new PrintWriter(clientSocket.getOutputStream(), true);

            // Khởi tạo luồng đọc từ bàn phím của server
            consoleReader = new BufferedReader(new InputStreamReader(System.in));
            System.out.println("Enter messages to send to the client:");

            // Khởi động luồng để nhận tin nhắn từ client
            ClientMessageHandler clientMessageHandler = new ClientMessageHandler();
            clientMessageHandler.start();

            // Gửi tin nhắn từ server tới client
            String message;
            while (true) {
                message = consoleReader.readLine();
                out.println(message);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    static class ClientMessageHandler extends Thread {
        public void run() {
            try {
                String clientMessage;
                while ((clientMessage = in.readLine()) != null) {
                    System.out.println("Message from client: " + clientMessage);
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
}