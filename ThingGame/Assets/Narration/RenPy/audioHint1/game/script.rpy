# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define ff = Character('Frinchfry', color="#c8ffc8")
define tt = Character('The Thing', color="#ff0000")

# The game starts here.
label start:
    play voice "thing-line0.wav"
    tt "...(dark murmurs)..."

label line2:
    play voice "ff-dungeon-6.wav"
    ff "Sheesh, looks like it got these guys good. We need to find the source of those things."
label end:
    return
