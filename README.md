# End to End IoT Use Cases - Device to Cloud & Cloud to Device & Cloud to Streaming Hub to UI

## Solution Description
By running the solution from VS, all events would happen in sequences. 
Solution is pointing to a IoT hub created in azure subscription. Sequence are mentioned below.
1. CreateDeviceIdentity.proj => A console program for device registration.
2. SimulatedDevice.proj => A console program  to send data from Device to IoT Hub 
3. ReadDeviceToCloudMessages.proj => A console program to receive Data sent by device from IoT Hub  (Service bound) and stream data to SignalR hub 
4. SendCloudToDevice.proj => A console program to send data from cloud to device (device bound) and process the same data in device side
5. StreamingHub.proj => A console program to host a SignalR hub for real-time data streaming (self-hosting – OWIN, capable of CORS) 
6. JavascriptClient.csproj => A JavaScript client (ASP.NET project) to represent live streaming of device data 

###  Setup instructions
1. Create IOT HUB in Azure Subscription
2. Copy IOT HUB HOST Name from Azure and paste that in Program.cs file of SimulatedDevice.proj as instructed there.
3. Copy IOT HUB Connection string from Azure and paste that in Program.cs file 
of CreateDeviceIdentity.proj, ReadDeviceToCloudMessages.proj and SendCloudToDevice.proj
4. First time run  project CreateDeviceIdentity.proj and check the console output to get the device key. 
5. Copy this key and paste in Program.cs file of SimulatedDevice.proj as instructed there.
6. Stop the program.
7. Now onwards run all projects simultaneously and see things in action.



