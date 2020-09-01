import threading # for threading
import requests # for API call
import json # for processing api response
import time # for measuring time
import csv # for opening and reading zipcodes; not necessary for this program, but will be necessary in actual implementation

'''
Tutorial/Documentation Referred When Creating This:
https://www.tutorialspoint.com/python/python_multithreading.htm

Results of my run of this program:
    Using threads:  0:0:1.1738367080688477
    Serial:         0:0:2.097921848297119
'''

'''
Method to read csv zipcodes
'''
def get_all_zipcodes():
    return_zips = []
    with open('us_postal_codes.csv') as zipfile:
        reader = csv.DictReader(zipfile)
        [return_zips.append(row['Zip Code']) for row in reader]
    return return_zips # 40933 zipcodes, fits in memory as is; has not been tested with DB interaction of any sort

'''
Method returns a list containing sublists of zipcodes.
Sublists are of length 41 except for the last list which
    has a length of 15.
Each sublist should be given to the thread class (in this
    case, it would be the api_call_thread class)
'''
def get_all_zipcodes_grouped():
    all_zipcodes = get_all_zipcodes()
    all_zipcodes_grouped = []
    step = 41 # 41 zipcodes per thread
    [all_zipcodes_grouped.append(all_zipcodes[i:i+step]) for i in range(0, len(all_zipcodes)+1, step)]
    return all_zipcodes_grouped

'''
Helper function to assist in measuring runtime of functions
'''
def time_convert(sec):
    mins = sec // 60
    sec = sec % 60
    hours = mins // 60
    mins = mins % 60
    print("Time Lapsed = {0}:{1}:{2}".format(int(hours),int(mins),sec))



class api_call_thread(threading.Thread):

    def __init__(self, zipcode, key):
        threading.Thread.__init__(self) # must be called in class init
        self.zipcode = zipcode
        self.key = key

    def run(self):
        r = requests.get('https://api.co-opfs.org/locator/proximitysearch', params={'zip':self.zipcode}, headers={'Accept':'application/json', 'Version':'1', 'Authorization': self.key})
        response_as_json = json.loads(r.text)
        print("Records available in {}: {}".format(self.zipcode, response_as_json['response']['resultInfo']['recordsAvailable']))
        self.locations = response_as_json['response']['locations']
        self.print_terminalIds()

    def print_terminalIds(self):
        for location in self.locations:
            print("t-{}: \n\tterminalId: {}".format(self.zipcode, location['terminalId']))


if __name__ == "__main__":
    with open('key.txt', 'r') as file:
        key = file.read().strip()
    zipcodes = ['99349', '99350', '99352', '99353', '99354'] # small sample of 5

    start = time.time()
    threads = [api_call_thread(zipcode, key) for zipcode in zipcodes]
    [thread.start() for thread in threads] # begins running the thread. this calls api_call_thread.run()
    [thread.join() for thread in threads] # joins threads for an accurate measure of concurrency
    end = time.time()
    time_convert(end-start)