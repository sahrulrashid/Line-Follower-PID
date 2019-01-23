# LineFollowerPID
Simulation of line follower with PID in Unity using my own algorithm for overshoot problem. 
For bulding robot model I used wheel colliders and built in physics.

![alt text](https://github.com/vvrvvd/LineFollowerPID/blob/master/Screenshoots/race%20route.png)

![alt text](https://github.com/vvrvvd/LineFollowerPID/blob/master/Screenshoots/line%20follower%20ui.png)

After 60 seconds from unstopped simulation, it saves three files into main folder:
    parameters.txt - parameters of Kp, Ki and Kd
    error.txt - PID error values during simulation
    time.txt - time stamps between error calculation
You can easily plot an error chart with python matplotlib as shown below:

![alt text](https://github.com/vvrvvd/LineFollowerPID/blob/master/Screenshoots/error.png)

