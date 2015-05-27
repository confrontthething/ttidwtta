# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define ff = Character('Frinchfry', color="#c8ffc8")
define tt = Character('The Thing', color="#ff0000")

# The game starts here.
label start:
    play voice "ff-dungeon-6.wav"
    ff "Holy hamburger! We’re surrounded!"
    pause 0.2
    play voice "Thing_All_.mp3"
    tt "All this running away..."
    pause 0.2
    play voice "ff-dungeon-7.wav"
    ff "Umm... I think it's talking?"
    pause 0.2
label end:
    return
