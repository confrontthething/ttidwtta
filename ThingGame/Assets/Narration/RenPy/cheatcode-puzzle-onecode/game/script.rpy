﻿# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define ff = Character('Frinchfry', color="#c8ffc8")

# The game starts here.
label start:
    play voice "tensec-silence-track.wav"
    ff "Hmm... There’s a few more pieces of the code left to remember."

label end:
    return
