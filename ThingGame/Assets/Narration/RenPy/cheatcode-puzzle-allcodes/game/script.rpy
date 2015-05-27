# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define ff = Character('Frinchfry', color="#c8ffc8")

# The game starts here.
label start:
    play voice "ff-courtyard-6.wav"
    ff "Okay, buddy. Cheat away by using your ARROW KEYS!"

label end:
    return
