# LineFollowerPID
# A simulation of line follower with PID in Unity using my own algorithm for overshoot problem.  Robot movement is based on wheel colliders and physics built in Unity.

# [Presentation videos and charts(CLICK)](https://www.dropbox.com/sh/n9m6bl9dvuo0i83/AAAwWYt_6QBPf7ynb-Ycgg4Ra?dl=0)

![alt text](https://github.com/vvrvvd/LineFollowerPID/blob/master/Screenshoots/line%20follower%20ui.png)

![alt text](https://github.com/vvrvvd/LineFollowerPID/blob/master/Screenshoots/race%20route.png)

After 60 seconds from unstopped simulation, application saves three files into main folder:
    parameters.txt - parameters of Kp, Ki and Kd
    error.txt - PID error values during simulation
    time.txt - time stamps between error calculation
You can easily plot an error chart using python matplotlib as shown below:

![alt text](https://github.com/vvrvvd/LineFollowerPID/blob/master/Screenshoots/error.png)

