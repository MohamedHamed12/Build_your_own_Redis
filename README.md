



# Build your own Redis ( TCP Server)

This project is a simple Redis-like in-memory key-value store implemented using .NET. It supports basic Redis commands such as `GET`, `SET`, `PING`, and `ECHO`, along with an expiration feature for the `SET` command. Clients can connect using `telnet` to interact with the server. The project demonstrates key features such as:

- Basic command processing with `GET` and `SET` commands.
- In-memory key-value store with optional time-to-live (TTL).
- Proper error handling and Redis-style responses (`+OK`, `-ERR`).
- A Redis-style command-line prompt (`redis>`).
- Efficient exception handling using custom exceptions.

## Features

- **GET**: Retrieve the value of a key.
- **SET**: Store a key-value pair in memory, optionally with an expiration time.
- **PING**: A simple command to test if the server is running.
- **ECHO**: Return the input string as the response.
- **Custom Exceptions**: Provides detailed error messages following Redis format (`-ERR`).

## Technologies Used

- **.NET 8**
- **MemoryCache**: For storing key-value pairs with optional expiration.
- **TCP/IP**: Using `TcpListener` and `TcpClient` to handle client-server communication.

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Telnet client (for testing)

## Getting Started

1. **Clone the repository**:

    ``` bash
     git clone https://github.com/your-repo/redis-tcp-server.git cd redis-tcp-server
    ```

    
2. **Build and Run the server**:
    ```bash
    dotnet run
    ```
    
3. **Connect using Telnet**:
    
    Open another terminal window and connect to the server using `telnet`:
    
    ```bash
    telnet localhost 6379
    ```
    
    
4. **Use the commands**:
    
    Now that you're connected, you can issue the following commands:
    
    - **SET command**: Stores a key-value pair.
	  
        ```bash 
        redis> set mykey hello
        +OK
        ```

 
    - **GET command**: Retrieves the value of a key.

          redis> get mykey
          $5 hello
 

## Architecture
  ```bash 
    .
    ├── Commands
    │   ├── CommandProcessor.cs
    │   ├── Commands
    │   │   ├── EchoCommand.cs
    │   │   ├── GetCommand.cs
    │   │   ├── PingCommand.cs
    │   │   └── SetCommand.cs
    │   └── ICommand.cs
    ├── Core
    │   ├── ClientHandler.cs
    │   ├── CommandException.cs
    │   └── RedisServer.cs
    ├── Program.cs
    └── RedisServerApp.csproj
  ```


## Adding New Commands

To add a new command, follow these steps:

1. **Create a new class** in the `Commands` folder that implements the `ICommand` interface.
2. **Implement the logic** for your command in the `Execute` method.
3. **Register your command** in the `CommandProcessor` class.

