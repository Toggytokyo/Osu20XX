# Osu20XX
Osu20XX is a small program that uses machine learning to tell you what year's mapping style a map most closely resembles. Simply drag and drop a .osz or .osu file into the window and select the difficulty to see the results.

I'm pretty new to machine learning and this is my first real project involving it. I'm sure I violated a few industry best practices, so if you notice anything let me know :)  
That being said, the ML algorithm predicts the mapping style correctly 60% of the time and is usually only off by about 1-2 years when it is wrong.

I plan on adding more functionality to this into the future, including:
* General mapping style detection (pp, tech, etc)
* Map recommendations
* Customizable ML Model
* More


Included in the source code are the two Python files that I used to collect the data used to train the ML model. The BeatmapUnpacker was used to extract all of the files from the .zip files I got from here: https://osu.ppy.sh/community/forums/topics/330552?n=1 and rename the files based on their year. DataScraper collects all of the data from the unpacked files and compiles it to a tsv file that is eventually used to train the model. I plan on eventually making these files flexible so that users can create their own data points to train a custom model.
