#!/usr/bin/env bash
GREEN=$(tput setaf 2)
NC=$'\e[0m'
echo "${GREEN}Welcome to the Advanced Elevator Experience!${NC}"
sleep 5
echo "${GREEN}Building application...${NC}"
sleep 5
dotnet clean
dotnet build
echo "${GREEN}Clean and build successful, running tests...${NC}"
sleep 5
cd "ElevatorChallenge.Tests"
dotnet test
cd ..
echo "${GREEN}================================================${NC}"
echo "${GREEN}Tests complete! Running application...${NC}"
sleep 5
dotnet run