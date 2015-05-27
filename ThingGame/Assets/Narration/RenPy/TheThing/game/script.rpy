# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define e = Character('The Thing', color="#ff0000")


# The game starts here.
label start:
    pause 8.0
    play voice "silence-track.wav"
    e "Look me in the eyes."
    pause 4.0
     play voice "silence-track.wav"
    e "I said LOOK AT ME when I’m speaking to you!"

    return
