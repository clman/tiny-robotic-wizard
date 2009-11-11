#!/usr/bin/python
#coding: shift-jis

import yaml
from optparse import OptionParser		
import sys
import os

configFile = 'map.yml'
if( len(sys.argv) > 1 ):
	configFile = sys.argv[1]
	sys.argv.pop(1)

parser = OptionParser()
parser.add_option("-o", "--output", dest="output", default="map.h", help="出力ファイル名を指定する")
(options, args) = parser.parse_args()

s = open(configFile).read()
config = yaml.load(s)
outputName = os.path.splitext(os.path.split(options.output)[1])[0].upper()

output = open(options.output, 'w')
output.write("#ifndef __"+outputName+"_H__\n");
output.write("/**\n")
output.write(" * @file "+options.output+"\n")
output.write(" * \n")
output.write(" * このファイルはgpiomap.pyにより自動的に生成されました．\n")
output.write(" * ポートの割り当ての設定を行います．\n")
output.write(" */\n")
output.write("\n")

root = config['gpio']
outputPrefix = root['output_prefix']
inputPrefix = root['input_prefix']
directionPrefix = root['direction_prefix']

concatMacroName = outputName + "_CC"
portMacroName = outputName + "_OUT"
pinMacroName = outputName + "_IN"
ddrMacroName = outputName + "_DIR"
output.write("#define	"+concatMacroName+"(a, b)	a ## b\n")
output.write("#define	"+portMacroName+"(name)	"+concatMacroName+"("+outputPrefix+", name)\n")
output.write("#define	"+pinMacroName+"(name)	"+concatMacroName+"("+inputPrefix+", name)\n")
output.write("#define	"+ddrMacroName+"(name)	"+concatMacroName+"("+directionPrefix+", name)\n")
output.write("\n")

ports = root['ports']

for portKey in ports.iterkeys():
	port = ports[portKey]
	name = port['name']
	outReg = outputPrefix + name
	inReg = inputPrefix + name
	dirReg = directionPrefix + name
	
	bits = port['bits']
	for bit in bits.iterkeys():
		alternativeNames = bits[bit]['alternative_names']
		for alternativeName in alternativeNames:
			output.write("//definition for "+alternativeName+"\n")
			output.write("#define	PORTNAME_"+alternativeName+"	"+name+"\n")
			output.write("#define	BIT_"+alternativeName+"	"+str(bit)+"\n")
			output.write("#define	"+outputPrefix+"_"+alternativeName+"	"+portMacroName+"(PORTNAME_"+alternativeName+")\n")
			output.write("#define	"+inputPrefix+"_"+alternativeName+"	"+pinMacroName+"(PORTNAME_"+alternativeName+")\n")
			output.write("#define	"+directionPrefix+"_"+alternativeName+"	"+ddrMacroName+"(PORTNAME_"+alternativeName+")\n")
			output.write("\n")
			
output.write("\n")
output.write("#endif	//__"+outputName+"_H__\n");		
output.close()
