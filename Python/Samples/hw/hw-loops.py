list = [1,2,32,12,'1212']

for i in range(len(list)):
    print(list[i])

print(list[:-3])
print(list[-2:])
print(list[-2:-3])

# for (i=0; i<len(list); i++):
#     print(list[i])

newList = [i*i for i in range(5)]
print(newList)