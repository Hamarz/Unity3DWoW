﻿
using Client;
using Client.Authentication;
using Client.World;
using Client.World.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace Assets.Scripts.Shared
{
    public static class Exchange
    {
        //public static Realm currRealm;
        public static AutomatedGame gameClient = null;
        public static bool connected = false;
        public static bool disconnected = false;
        public static bool playerIsInGame = false;
        public static string selectedRace = "";
        public static string selectedClass = "";
        public static string selectedGender = "";
        public static string Username = "";
        public static string Password = "";
        public static WorldServerInfo CurrentRealm;

        public static string AuthMessage = "";

        public static Character newCharacter;
        public static Character SelectedCharacter;
        public static Character[] characters = null;
        public static AudioClip[] Sounds = null;
        public static string Name;
        public static byte Race;
        public static byte Class;
        public static byte Gender;
        
        public static BigInteger ToBigInteger(byte[] array)
        {
            byte[] temp;
            if ((array[array.Length - 1] & 0x80) == 0x80)
            {
                temp = new byte[array.Length + 1];
                temp[array.Length] = 0;
            }
            else
                temp = new byte[array.Length];

            Array.Copy(array, temp, array.Length);
            return new BigInteger(temp);
        }

        public static byte[] ToCleanByteArray(BigInteger b)
        {
            byte[] array = b.ToCleanByteArray();
            if (array[array.Length - 1] != 0)
                return array;

            byte[] temp = new byte[array.Length - 1];
            Array.Copy(array, temp, temp.Length);
            return temp;
        }

        public static bool IsConnected
        {
            get
            {
                try
                {
                    if (Exchange.gameClient.socket.connection != null && Exchange.gameClient.socket.connection.Client != null && Exchange.gameClient.socket.connection.Client.Connected)
                    {
                        // Detect if client disconnected
                        if (Exchange.gameClient.socket.connection.Client.Poll(0, SelectMode.SelectRead))
                        {
                            byte[] buff = new byte[1];
                            if (Exchange.gameClient.socket.connection.Client.Receive(buff, SocketFlags.Peek) == 0)
                            {
                                // Client disconnected
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
