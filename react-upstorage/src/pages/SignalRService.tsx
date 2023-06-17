import { HubConnectionBuilder, LogLevel, HubConnection } from '@microsoft/signalr';

class SignalRService {
    private connection: HubConnection | null = null;

    public startConnection() {
        this.connection = new HubConnectionBuilder()
            .withUrl('https://localhost:7078/Hubs/ChatHub') // Replace with your SignalR endpoint URL
            .configureLogging(LogLevel.Information)
            .build();

        this.connection.start()
            .then(() => {
                console.log('SignalR connection established');
                // Add event handlers or perform any necessary setup after the connection is established
            })
            .catch((error) => {
                console.log('Error establishing SignalR connection:', error);
            });
    }

    public stopConnection() {
        if (this.connection) {
            this.connection.stop()
                .then(() => {
                    console.log('SignalR connection stopped');
                    // Clean up any resources or perform additional actions after stopping the connection
                })
                .catch((error) => {
                    console.log('Error stopping SignalR connection:', error);
                });
        }
    }

    public getConnectionStatus(): string {
        if (this.connection) {
            switch (this.connection.state) {
                case signalR.HubConnectionState.Connected:
                    return 'Connected';
                case signalR.HubConnectionState.Connecting:
                    return 'Connecting';
                case signalR.HubConnectionState.Disconnected:
                    return 'Disconnected';
                case signalR.HubConnectionState.Reconnecting:
                    return 'Reconnecting';
            }
        }
        return 'Not connected';
    }




    public async invokeGetAllUsers(): Promise<string[]> {
        try {
            if (this.connection) {
                console.log("getAll");
                const result = await this.connection.invoke<string[]>('GetAllUsers');
                return result;
            } else {
                throw new Error('SignalR connection not established');
            }
        } catch (error) {
            console.log('Error invoking GetAllUsers:', error);
            return [];
        }
    }

    public async invokeAddUser(user : string): Promise<string> {
        try {

            if (this.connection) {
                console.log("user", user);
                const result = await this.connection.invoke<string>("UserConnected", user);
                return result;
            } else {
                throw new Error('SignalR connection not established');
            }
        } catch (error) {
            console.log('Error invoking GetAllUsers:', error);
            return "Error";
        }
    }



    // Add any additional methods or event handlers for interacting with SignalR
}

export default SignalRService;
