# Angle of Pursuit
Build a system that calculates an Angle of Pursuit that holds back or intercepts the Player from making a touchdown.

## Initial Approach
To employ the existing NavMesh System available in Unity to define movement behaviours and build on top of it.

Initial inspiration was one of Pac-Man's Ghosts, Pinky. Pinky is programmed to target 4 tiles infront of Pac-Man to make it seem like Pinky is aware of the player's next move. This was used but it wasn't effective and most of the times the Defender beelined towards the player instead of accurately intercepting them.

The post used for studying Pac-Man behaviours can be [found here](http://gameinternals.com/understanding-pac-man-ghost-behavior)

## Process

From the Pac-Man idea the Player and Defender's interaction was mostly treated as a 2D system leading to freebody diagrams and sketches to visualise movement. This later developed into diagrams of triangles and subsequently lead to exploring Pythagorean Theroem to predict the Player's future position. This can be seen in NBC Learn's series [Science of NFL Football](https://www.nsf.gov/news/mmg/mmg_disp.jsp?med_id=71403&from=) where Dr. John Ziegert discusses the relation between the two. With this a new line of thinking was approached where instead of dealing with the Player and Defender as physics systems it was treated as Mathematical systems, specifically in the form of vectors, cosines and magnitudes.

## Solution
After some researching online, a few explainer videos and articles I eventually came across a simple and elegant solution that used Quadratic Equations and The Law of Cosines to solve this problem. The author of the blog derived inspiration for this problem from Looney Toon's Roadrunner and Coyote. What if the Coyote could catch up. This was the solution that I needed to code this system. I worked through the problem myself step by step along with the author and then wrote code along with the edge cases for this problem in Unity.

The link to the post [Interception of Two Moving in 2D Space] can be found [here](https://www.codeproject.com/Articles/990452/Interception-of-Two-Moving-Objects-in-D-Space).

## Important Functions
```CalculateQuadEqn()``` is present in the ```AngleOfPursuitDefender.cs``` script. This function handles the math that employs the known variables Defender speed, Player speed & direction/Player Velocity, Defender's Position Vector, Player's Position Vector and their magnitudes to find the Time of Interception _t_. This function also handles the edge cases arising due to negative values of _t_ and imaginary values of the Quadratic Equation.

```CalculatePointOfIntersectionAndDefenderVelocity()```, also found in ```AngleOfPursuitDefender.cs```, is used to calculate the Point of Interception and that is used to find the Velocity of the Defender. This is then used to override the existing velocity emerging out of NavMesh's ```SetDestination()``` and direct the Defender in the right Angle of Pursuit to intercept the player.
