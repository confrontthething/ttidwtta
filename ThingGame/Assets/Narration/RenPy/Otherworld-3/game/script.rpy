# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.

define ff = Character('Frinchfry', color="#c8ffc8")
define e = Character('The Thing', color="#ff0000")

# The game starts here.
label start:

    play voice "ff-hubworld2-17.wav"
    ff "Wha- where are we?"

    play voice "Thing_DontYou2_.mp3"
    e "Don't you know... I am a part of you?"

    play voice "Thing_IAm_.mp3"
    e "A feeling, a memory, locked inside."

    play voice "ff-hubworld2-19.wav"
    ff "Don't let it in. It's up to you to keep it out now! Don't listen to it!"

    play voice "Thing_Fathers_.mp3"
    e "He can't hurt you anymore..."

    play voice "Thing_Let_.mp3"
    e "Now... Let... me... IN!"

    play voice "FF_IWont_.mp3"
    ff "I won't let you take my friend away! It's not safe out there!"

    play voice "FF_Stay_.mp3"
    ff "Stay with ME!"

label end:
    return
