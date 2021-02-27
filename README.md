# ArchiveBruteforce
Program demonstruje metodę BruteForce'a do odzyskania hasła archiwum, na podstawie wszystkich możliwych kombinacji zaczynając od minimalnej długości dopóki nie znajdzie poprawne hasło. Praca z archiwum wykonywana za pomocą biblioteki [Ionic.ZipLibrary](https://documentation.help/DotNetZip/)    

# Instrukcja
Zwykły program demonstrujący działanie tzw. BruteForce'a który polega na sprawdzaniu wszystkich haseł. Ten program pomaga znaleźć hasło do plików zapakowanych **(.zip)**. 
1. Wrzucić archiwum w pole drag&drop
2. Nacisnąć przycisk **Start Bruteforce**

# Algorytm działania BruteForce'a 💀
### Główna pętla sprawdzania jest tutaj czyli sprawdzamy po kolei wszystkie możliwości dla określonej długości:
```c#
while(!isMatched)
{
    passLength++;
    StartBruteForce(passLength);
}
```
### Funkcja generuje przykładowe hasło określonej dlugości np. dla *length = 2*, przykładowe hasło : *aa*  
```c#
private static char[] createCharArray(int length, char defaultChar)
{
    return (from c in new char[length] select defaultChar).ToArray();
}
```

### Funkcja generowania wszystkich możliwości hasła określonej długości
```c#
private static void CreateNewKey(int currentCharPosition, char[] keyChars, int passLength, int indexOfLastChar)
{
    int nextCharPosition = currentCharPosition + 1;
    for (int i = 0; i < charactersToTestLength; i++)
    {
        keyChars[currentCharPosition] = charactersToTest[i];
        if(currentCharPosition<indexOfLastChar)
        {
            CreateNewKey(nextCharPosition, keyChars, passLength, indexOfLastChar);
        }
        else
        {
            computedKeys++;
            pass = new string(keyChars);
            if (ZipFile.CheckZipPassword(archive.Name, pass))
            {
                if(!isMatched)
                {
                    isMatched = true;
                    MessageBox.Show($"Password is {new string(keyChars)}");
                }
                return;
            }
        }
    }
}
```
**currentCharPosition** - odpowiada za przesuwanie po indeksach hasła  
**keyChars** - przechowuje hasło  
**indexOfLastChar** - pozwala sprawdzać czy jesteśmy w końcu określonej dlugości hasła  

Czyli gdy mamy np. sprawdzić wszystkie możliwości dla długości równej dwa:  
1. W **keyChars[0]** wstawiane po kolei symbole alfabetu  
2. Z tego powodu **currentCharPosition < indexOfLastChar** to przechodzimy do kolejnej pozycji i już sprawdzamy wszystkie **2** elementowe możliwości, następnie gdy funkcja powróci z rekursji
w **keyChars[0]** będzie wstawiony następny symbol alfabetu i już dla tego symbola przechodimy do pozycji drugiej i sprawdzamy wszystkie **2** elementowe możliwości.

# Screenshot
![Screenshot_1](https://user-images.githubusercontent.com/19534189/109402522-e02b1400-7956-11eb-8408-9c23da76ad36.png)
