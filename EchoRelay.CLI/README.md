# EchoRelay.CLI

This console app provides a "headless" mode into the Echo Relay application, removing any need for a GUI application.

To view all the commands in the application, use the `help` command.

# How do I launch the server then?

You can launch the server with the `server_start` command and using the `-port` and `-apiKey` arguments.
A valid command would be `server_start -port 777` or `server_start -port 777 -apiKey API_KEY_HERE`

When using the `-apiKey` argument, make sure your key is in Base64 format! If it isnt, the server will fail to start.

Likewise, to stop the server, use the `server_stop` command. Using this command will stop every
Echo service running on your computer.