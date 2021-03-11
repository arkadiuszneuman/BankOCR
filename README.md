# BankOCR

To run app go to `./BankOCR` (where `BankOCR.csproj` is located) directory and run:
```
dotnet run
```

File `default-data.txt` will be used as scanned data, in `BankOCR/bin/Debug/net5.0/results.txt` you'll find results.

You can use your own data by switch `-d` or `--data` to set location to your scanned data.

You can use your own results file by switch `-r` or `--result` to set location of output data.

```
dotnet run -d "some/path/to/scanned-data.txt" -r "some/path/to/result-file.txt"
```

## Docker

You can also run program in docker. First you have to build an image:
```
docker build -t bankocr -f Dockerfile .
```

Then run it by:
```
docker run -v C:/some/path/to/your/resuts/directory:/app/data bankocr -r "data/results.txt"
```