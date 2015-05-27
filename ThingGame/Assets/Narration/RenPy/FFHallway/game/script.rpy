# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define ff = Character('Frinchfry', color="#c8ffc8")
define tt = Character('The Thing', color="#ff0000")

# The game starts here.
label start:
    play voice "ff-dungeon-7.wav"
    ff "Ahh! Sorry, I thought I saw a spider!"
    pause 0.2
    play voice "ff-dungeon-8.wav"
    ff "Enemies up ahead!"
    pause 0.2
    play voice "Thing_Find_.mp3"
    tt "Find me and end this."
    play voice "ff-dungeon-9.wav"
    ff "Yeah, we’ll find you. And we’ll get rid of you once and for all!"
label end:

    return
