# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define ff = Character('Frinchfry', color="#c8ffc8")
define me = Character('Me', color="#c8ffc8")


# The game starts here.
label start:
    play voice "ff-line1.wav"
    ff "Hiyah, friend. Remember me? It’s okay if you don’t. It’s been a long time..."
    pause 0.2

label line2:
    play voice "ff-line2.wav"
    ff "No?? I’m Frinchfry!! Your best friend! Only a little imaginary."
    pause 0.2

label line3:
    play voice "ff-line2-1.wav"
    ff  "So that thing is pretty freaky... Almost gotcha, huh? But I got your back, bud."
    pause 0.2


label line4:
    play voice "ff-line2-2.wav"
    ff  "When things got tough back in the day, we always came here together. But then--"
    pause 0.2

label line5:
    play voice "ff-line3.wav"
    ff "Nevermind that! So I have some bad news and some good news."
    pause 0.2

label line6:
    play voice "ff-line3-1.wav"
    ff "The bad: part of that thing got in here. Just a teensy bit. The good: he’s hiding in your favorite arcade game!"
    pause 0.2

label line7:
    play voice "ff-line5.wav"
    ff "You remember the one don’t you?? Yeahhh of course you do. So I say we go and beat him up."
    pause 0.2

label line8:
    play voice "ff-line5-1.wav"
    ff "You can hear the rest of him banging on the door, but don’t pay much attention to it. I locked it out."
    pause 0.2

label line9:
    play voice "ff-line5-2.wav"
    ff "As long as we get rid of the piece that got in, it’ll stop trying so hard."
    pause 0.2

label end:

    return
