# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.

define ff = Character('Frinchfry', color="#c8ffc8")
define tt = Character('The Thing', color="#ff0000")

# The game starts here.
label start:

    play voice "ff-hubworld2-22.wav"
    ff "Oh man... that thing really messed me up. I’m sorry."

    play voice "ff-hubworld2-23.wav"
    ff "I just want you to be happy, you know... With me, you won’t have to think about 	anything but having fun."

    play voice "ff-hubworld2-24.wav"
    ff "We don't even know what that thing is. I think we scared it off for now. Let's 		close the door and keep it out for good this time."

    play voice "ff-hubworld2-25.wav"
    ff "What do you say? Stay with me?"


label end:
    return
