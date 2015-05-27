# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define ff = Character('Frinchfry', color="#c8ffc8")

# The game starts here.
label start:
    play voice "fivesec-silence-track.wav"
    ff "Here’s where we should use the cheat code."

label line2:
    play voice "fivesec-silence-track.wav"
    ff "Maybe if you walk around more, you’ll remember what the code was?"

label end:
    return
