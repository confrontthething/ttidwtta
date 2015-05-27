# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define ff = Character('Frinchfry', color="#c8ffc8")

# The game starts here.
label start:
    play voice "ff-dungeon-2.wav"
    ff "Let’s go get ’em!"
    pause 0.2
label end:

    return
