import java.io.*;
import java.net.*;

public class Server {
    public static void main(String[] args) {
        try {
            ServerSocket serverSocket = new ServerSocket(1234);
            System.out.println("Server started. Waiting for clients...");

            while (true) {
                Socket clientSocket = serverSocket.accept();
                System.out.println("Client connected: " + clientSocket);

                ClientHandler clientHandler = new ClientHandler(clientSocket);
                clientHandler.start();
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}

class ClientHandler extends Thread {
    private Socket clientSocket;
    private PrintWriter out;
    private BufferedReader in;
    private String username;

    public ClientHandler(Socket clientSocket) {
        this.clientSocket = clientSocket;
    }

    public void run() {
        try {
            out = new PrintWriter(clientSocket.getOutputStream(), true);
            in = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));

            // Yêu cầu client nhập username
            out.println("Please enter your username:");
            username = in.readLine();
            System.out.println("Client username: " + username);

            String inputLine;
            while ((inputLine = in.readLine()) != null) {
                System.out.println("Received message from " + username + ": " + inputLine);

                // Gửi tin nhắn cho tất cả client khác
                for (ClientHandler client : ServerManager.getClients()) {
                    if (client != this)
                        client.sendMessage(username + ": " + inputLine);
                }
            }

            out.close();
            in.close();
            clientSocket.close();
            System.out.println("Client disconnected: " + clientSocket);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void sendMessage(String message) {
        out.println(message);
    }
}

class ServerManager {
    private static List<ClientHandler> clients = new ArrayList<>();

    public static synchronized void addClient(ClientHandler client) {
        clients.add(client);
    }

    public static synchronized void removeClient(ClientHandler client) {
        clients.remove(client);
    }

    public static synchronized List<ClientHandler> getClients() {
        return clients;
    }
}