import java.io.*;
import java.net.*;
import java.text.SimpleDateFormat;
import java.util.Date;

public class Server {
    private static Socket clientSocket;
    private static BufferedReader in;
    private static PrintWriter out;

    public static void main(String[] args) {
        try {
            ServerSocket serverSocket = new ServerSocket(1234);
            System.out.println("Server started. Waiting for client...");

            clientSocket = serverSocket.accept();
            System.out.println("Client connected: " + clientSocket);

            in = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
            out = new PrintWriter(clientSocket.getOutputStream(), true);

            String clientMessage;
            while ((clientMessage = in.readLine()) != null) {
                if (clientMessage.equals("time")) {
                    String currentTime = getCurrentTime();
                    out.println(currentTime);
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private static String getCurrentTime() {
        SimpleDateFormat formatter = new SimpleDateFormat("HH:mm:ss");
        Date date = new Date();
        return formatter.format(date);
    }
}