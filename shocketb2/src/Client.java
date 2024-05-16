import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.*;
import java.net.*;

public class Client extends JFrame {
    private static final int WIDTH = 300;
    private static final int HEIGHT = 200;

    private JLabel timeLabel;
    private Timer timer;
    private Socket socket;
    private BufferedReader in;
    private PrintWriter out;

    public Client() {
        setTitle("Clock");
        setSize(WIDTH, HEIGHT);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        timeLabel = new JLabel();
        timeLabel.setFont(new Font("Arial", Font.PLAIN, 24));
        timeLabel.setHorizontalAlignment(SwingConstants.CENTER);
        add(timeLabel, BorderLayout.CENTER);

        timer = new Timer(1000, new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                sendTimeRequest();
            }
        });
    }

    private void sendTimeRequest() {
        try {
            out.println("time");
            String serverResponse = in.readLine();
            timeLabel.setText(serverResponse);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void start() {
        try {
            socket = new Socket("localhost", 1234);
            in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            out = new PrintWriter(socket.getOutputStream(), true);

            timer.start();

            setVisible(true);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public static void main(String[] args) {
        Client client = new Client();
        client.start();
    }
}