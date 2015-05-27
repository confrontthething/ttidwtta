# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define ff = Character('Frinchfry', color="#c8ffc8")
define tt = Character('The Thing', color="#ff0000")

# The game starts here.
label start:

label line1:
    play voice "1sec-thing-shriek.wav"
    tt ".....(menacing shriek)......"
    pause 0.2

label line2:
    play voice "ff-dungeon-1.wav"
    ff "Hey, buddy. I’ll help you from out here! Emotional support, you know?"

label line3:
    play voice "ff-dungeon-2.wav"
    ff "Let’s figure out where that thing went!"

label retControl:
label end:
    return
