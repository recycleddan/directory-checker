import os
import sys
import hashlib

startingDir = sys.argv[1]
for root, subfolders, files in os.walk(startingDir):
    for name in files:
        print '{0}'.format(hashlib.md5(open(os.path.join(root, name), 'rb').read()).hexdigest())