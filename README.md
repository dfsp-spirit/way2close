# Way2Close
Way2Close -- a simple Unity space action game project.


## About
Way2Close is a space game about avoiding to crash into obstacles and enemies -- but you get points for flying close to them.

The gameplay of Way2Close in inspired by some one-button game a friend of mine had on his Palm in 2002. The graphics style is inspired by a demo for Processing.js, from which I built an early version of the game called *pRace* that [can be played here directly in the browser](http://rcmd.org/projects/prace/).

I built it to have a look at C# and at the Unity engine. The game is quite simple, it has only very limited sound effects and graphics, but it's fully playable and has a high score list. 

![Vis](./release/v0.2.0/screenshots/phone/way2close_tutorial.jpg?raw=true "Way2Close gameplay, totorial level.")
![Vis](./release/v0.2.0/screenshots/phone/way2close_gameplay.jpg?raw=true "Way2Close gameplay.")
![Vis](./release/v0.2.0/screenshots/phone/way2close_menu.jpg?raw=true "The Way2Close menu.")


## Installation

### Android
You can download an APK from the releases section here at Github. Just open it on your Android device to install it. If you prefer to build the APK yourself from the source code, see below.

### Other platforms
The Unity engine can generate output for a number of platforms, but I only have Android mobile devices (phone and tablet, no wearables, no TV). And I can only offer builds for platforms I own. I doubt that somebody wants to play the game on PC, but if you *really* want to, create an issue for it and I will provide builds for Windows, Linux and Mac. If you feel up to it, you can also create the build yourself of course, see below.

## Building from the source code
* Install the Unity game engine and a recent SDK for the supported platform you need (e.g., the Android SDK).
* Clone the source code from the repo here at Github. Let's call the resulting folder `<repo>`.
* Start Unity, choose to open an existing project, and select the folder `<repo>/Way2Close`.
* Configure and run the build (`File => Build Settings...` in Unity). This will produce the package (APK if your platform is Android).

## License
Way2Close is published under the GPL v3. See the LICENSE.md file for the full license.
