# Virtual Meltdown 2020
## GameJam 2020 Entry

### Overview

Virtual Meltdown is a game built by a group of St. Louis developers over the course of the Global Game Jam 2020 weekend 2020 (January 31st - February 2nd, 2020).   The game itself is a puzzle game which challenges a group of players to repair an overheating reactor in a futuristic space ship.  The technologies included incorporate multiple players playing together on a common game screen using their smart phones as "virtual" controllers.  Each player can connect to the common game and use their local device as a rich interface to move their character and make "deposits" of coolant cores into a one of a number of colored reactors.   A dedicated player can play "inside" the game using a full VR rig.  This player is required too help guide the remote players as to the state of the reactor cores and which need more coolant (and of what color).

The primary development team of the Virtual Meltdown application are coworkers at the St. Louis development firm [ArchitectNow](http://www.architectnow.net) where they design and build large enterprise and commercial applications for customers across the US.

### Team Members

Design and Development

- Chau Tran
- Guy Goodpaster
- Kevin Brack
- Kevin Grossnicklaus
- David Schwarb

Audio and Sound Effects

- Isaac Vandyne
- CJ Maus
- Zach Zito

### Design Considerations
The "theme" for the Global Game Jam 2020 event was announced at 5pm on January 31st as "Repair".  Working with this theme we envisioned a sci-fi game where players work to repair overheating nucleur reactor on a space station.  While the game has some complex technical and team components the overall gameplay is fairly simple and "casual".   

One goal we had initially was to build a team-based game where multiple players can collaborate using a single user interface but use their own personal smart devices (phones or tablets) as their controllers.  This idea was driven heavily by the popularity of the [JackBox Games](https://jackboxgames.com/) available on a wide number of platforms.  We felt that allowing players to use their own devices as controllers expanded greatly the unique types of experiences we could provide and a number of unique gameplay options.  We can "hide" information from the main screen or other players and present context aware options depending on where users are in the game.   We also wanted a social game where multiple players viewed the same central screen and interacting with it using their own phone.  We wanted to encourage local communication (yes, talking or yelling) while working towards or against each other.

Another goal we had was to incorporate a VR player into our environment and allow them to contribute to the game while seeing the remote players in the same environment.  Too many VR games are solo games that have very little to offer a group of people wanting to collaborate.  Virtual Meltdown is a start down the path of finding ways to allow the VR player to collaborate with other players in the same environment.

### Technologies

The following teachnologies were used to build the game:

- Development Tools:
	- Unity 3D (Game host and UI)
	- JetBrains Rider (C#)
	- JetBrains Webstorm (TypeScript/Node)

- Primary Frameworks/Engines
	- Unity 3D
	- Angular (Mobile controller)
	- Colyseus (Server-side Multiplayer Support)
	- Steam VR Plugin for Unity (VR Support)

- Dev Ops
	- Azure Dev Ops (Git and Agile boards)
	- Microsoft Teams (Communication and documentation)
	- Azure Pipelines (Automated builds and deployments)

- Key Assets
	- Bots and Turrets Sci-Fi Pack [Unity Asset Store for $14](https://assetstore.unity.com/packages/3d/characters/robots/bots-and-turrets-sci-fi-pack-100195) 
	- SCI-FI Battery Pack [Unity Asset Store for $20](https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-battery-pack-12231)
	- Sci Fi Top Down Space Station PolygonR [Unity Asset Store for $25](https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-top-down-space-station-polygonr-65449) 
	
### Architecture

The game itself consists of three main components:  the server, client, and remote player controllers.   The following sections provide a brief overview of each of these pieces and how theyinteract.

#### Server

The server technology used for Virtual Meltdown is a built heavily around an robust and open-source technology called [Colyseus](https://colyseus.io/).  Colyseus is a server-side technology built on top of Node and Express to help game developers synchronize game state between players or components via a real-time socket refresh.   using Colyseus we created a server platform for the game and exposed a single room which hosted a subset of our game logic.   The remote player controllers (developed as a rich web app via Angular and TypeScript) connect to the Colyseus server and send movement information (when a user moves the joystick) or other interaction information (button pushes, etc). The client component (written in Unity) also connects to the same Colyseus server.  This centralized component allows both the web-based controllers and the game to communicate in real time and keeps everyone in sync.

#### Client

The client application serves as the primary game user interface.  This piece of the overall puzzle.  This component was built heavily within Unity 3D and (due to the time constraints) was designed and built using a collection of existing assets we purchased from the Unity Asset store.  This allowed us to focus quickly on game play and leverage the skillsets of our existing team (which lacked heavy UI expertise).  

Once a game starts the client establishes a connection to our remote server (built with Colyseus) and creates a "room".  This room is given a unique four-digit code which is displayed on the primary game screen.  Remote players can navigate to a specific URL and enter their name, this room code, and choose one of three playable robots.  Once this is done they are placed in the primary game space where they can navigate by using the joystick on their phone and see the results on the main display.

The client manages all in-game physics, collision detection, audio, VR, and other logic.  It communicates efficiently both to and from all connected players via the Colyseus socket server.  The tooling provided by Colyseus made it easy for us to keep our Unity C# state objects in sync with our TypeScript models on the server (and also the TypeScript models in the Angular code for the controller.

Within the client project we have a single play field but two camera views:  one for the VR player and one for the remote players (who all view the same thing).   This allows us to hide things from different player types (and also leverage the fact that the remote players have their own custom UI on their devices which is unique to them).

A number of great audio producers stopped by our table during the jam and volunteered to put together all audio and sound Fx assets for us.  We agreed, they pulled up a table, and nearly all sound and effects you hear in the game are uniquely built by these guys (names above).

#### Remote Player Controller

The remote player component is a rich web app built on the Angular framework in TypeScript.  All graphics used in the custom controller UI was had designed as vector-based SVG files specific for our needs.   The touch-enabled "joystick" used by players was built using a JavaScript library called [NippleJs](https://yoannmoi.net/nipplejs/).  Using this library we customized their actual graphics setup to use our own SVG implementation to maintain the style we wanted for the controller.

All interaction between the remote controller and the other components is done via the Colyseus server.


