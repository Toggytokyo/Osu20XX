# !!! - OUTDATED SCRIPT - NEEDS UPDATE !!!

import os
import re
import csv
import math
import random
import statistics

dir_name = dir_name = input("Directory of .osu files to scrape data from: ")
dir_content = os.listdir(dir_name)

#Creating header format
header = ['year', 'hp', 'cs', 'od', 'ar', 'avgDeltaTime', 'stddevDeltaTime', 'diffVariance']

#Creating each data file
with open('data.tsv', 'w', newline='') as out_file:
    tsv_writer = csv.writer(out_file, delimiter='\t')
    tsv_writer.writerow(header)
with open('training.tsv', 'w', newline='') as out_file:
    tsv_writer = csv.writer(out_file, delimiter='\t')
    tsv_writer.writerow(header)
with open('test.tsv', 'w', newline='') as out_file:
    tsv_writer = csv.writer(out_file, delimiter='\t')
    tsv_writer.writerow(header)

#Reading in the data from the raw maps  and writing it to data.tsv
amt_err = 0
with open('data.tsv', 'a', newline='') as out_file:
    tsv_writer = csv.writer(out_file, delimiter='\t')
    for map in dir_content:
        hp = cd = od = ar = avgDeltaTime = stddevDeltaTime = diffVariance = "?"
        with open(os.path.join(dir_name, map)) as map_reader:
            try:
                osu_file = map_reader.readlines()
            except UnicodeDecodeError:
                amt_err+=1
                print("Could not open", amt_err, "file(s) due to invalid characters.", end='\r')
                continue
            for index, line in enumerate(osu_file):
                if re.match("(HPDrainRate:)(\d*\.?\d*)", line):
                    hp = re.match("(HPDrainRate:)(\d*\.?\d*)", line).group(2)
                if re.match("(CircleSize:)(\d*\.?\d*)", line):
                    cs = re.match("(CircleSize:)(\d*\.?\d*)", line).group(2)
                if re.match("(OverallDifficulty:)(\d*\.?\d*)", line):
                    od = re.match("(OverallDifficulty:)(\d*\.?\d*)", line).group(2)
                if re.match("(ApproachRate:)(\d*\.?\d*)", line):
                    ar = re.match("(ApproachRate:)(\d*\.?\d*)", line).group(2)
                if re.match("(\[[Hh]it[Oo]bjects\])", line):
                    first_line = re.match("(-?\d+\.?\d*),(-?\d+\.?\d*),(-?\d+\.?\d*)",osu_file[index+1])
                    last_x = float(first_line.group(1))
                    last_y = float(first_line.group(2))
                    last_time = float(first_line.group(3))
                    delta_times = []
                    diffs = []
                    for hit_object in osu_file[index+2:len(osu_file)]:
                        match_data = re.match(" ", hit_object)
                        current_x = float(match_data.group(1))
                        current_y = float(match_data.group(2))
                        current_time = float(match_data.group(3))
                        distance = math.sqrt((current_x-last_x)**2 + (current_y-last_y)**2)
                        delta_time = current_time-last_time
                        if delta_time != 0:
                            diff = distance/delta_time
                            diffs.append(diff)
                        delta_times.append(delta_time)
                        last_x = current_x
                        last_y = current_y
                        last_time = current_time
                    avgDeltaTime = sum(delta_times) / len(delta_times)
                    stddevDeltaTime = statistics.stdev(delta_times)
                    if len(diffs) != 0 and sum(diffs) != 0:
                        diffVariance = statistics.stdev(diffs) / (sum(diffs) / len(diffs))
                    else:
                        diffVariance = 0
                    break

        #Old maps don't list AR, assign default value
        if(ar == '?'):
            ar = "6"
        tsv_writer.writerow([map[0:4], hp, cs, od, ar, avgDeltaTime, stddevDeltaTime, diffVariance])

#escaping print loop
print("\n")

#Separating the data into a testing and a training file randomly
with open('data.tsv', 'r') as in_file:
    read_tsv = csv.reader(in_file, delimiter="\t")
    training_tsv = open('training.tsv', 'a', newline='')
    test_tsv = open('test.tsv', 'a', newline='')
    training_tsv_writer = csv.writer(training_tsv, delimiter="\t")
    testing_tsv_writer = csv.writer(test_tsv, delimiter="\t")

    for row in read_tsv:
        if row == header or row.count == 0:
            continue
        r = random.random()
        if r < 0.7:
            training_tsv_writer.writerow(row)
        else:
            testing_tsv_writer.writerow(row)

input("Press Enter to continue...")