# The game starts here.
# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define ff = Character('Frinchfry', color="#c8ffc8")
define tt = Character('The Thing', color="#ff0000")

# The game starts here.
label start:
    play voice "ff-dungeon-10.wav"
    ff "Ha! This old thing’s got nothing on us! I bet you’re a pro at it now!"
    pause 0.2
    play voice "ff-dungeon-11.wav"
    ff "...Please don’t mess up as much as you used to..."
    pause 0.2
label end:
    return
