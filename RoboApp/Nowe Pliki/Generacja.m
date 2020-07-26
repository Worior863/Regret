%% Zmieniamy rozmiar wykresu
axis([0 1900 0 1900])
set(gca,'XColor', 'none','YColor','none');
set(gca,'xtick',[])
set(gca,'ytick',[])
pbaspect([1 1 1 ])
[imageData, alpha] = export_fig(gcf);%Pobieram zdj�cie wykresu
imwrite(imageData,"JK.bmp"); %Zapisuje go do pliku
close all

IMAGE_SIZE = 1 * 1900;
POINT_FREE_ZONE = 47.5;

clear droga; %Czy�cimy macierz ze �cie�k�
X_We=0;%Wektor ze wsp�rz�dnymi X w�z��w
Y_We=0;%Wektor ze wsp�rz�dnymi Y w�z��w
Inkre_1=1;%Zmienna wektorowa ikrementowana do wype�niania macierzy
Inkre_2=1;%Zmienna wektorowa ikrementowana do wype�niania macierzy

%Pierwszy w�ze� wype�niamy wsp�lrz�dnymi robota
wezly(1,1)=1;
wezly(1,2)=X; % Pobrane z visual studio
wezly(1,3)=Y; % Pobrane z visual studio
%Ostatni w�ze� wype�niami wsp�rz�dnymi wsp�rz�dnymi punktu ko�cowego
wezly(50,1)=50;
wezly(50,2)=Xk; % Pobrane z visual studio
wezly(50,3)=Yk; % Pobrane z visual studio

%% Sprawdzamy czy te dwa w�z�y nie znajduj� si� przy kraw�dziach sto�u, gdzie robot nie ma dost�pu, je�li tam s� skrypt jest przerywany i zwracane jest 1 w zmiennej, do kt�rej zapisywana jest �cie�ka
if wezly(1,2,:)< POINT_FREE_ZONE || ...
   wezly(1,3,:)< POINT_FREE_ZONE || ...
   wezly(1,2,:)> IMAGE_SIZE - POINT_FREE_ZONE || ...
   wezly(1,3,:)> IMAGE_SIZE - POINT_FREE_ZONE
    droga=1;
    return;
end
if wezly(50,2,:) < POINT_FREE_ZONE || ... 
   wezly(50,3,:)< POINT_FREE_ZONE || ...
   wezly(50,2,:)> IMAGE_SIZE - POINT_FREE_ZONE || ...
   wezly(50,3,:)> IMAGE_SIZE - POINT_FREE_ZONE 
   
    droga=1;
    return;
end
%% Losujemy w�z�y
for J=2:(size(wezly,1)-1)
   
   wezly(J)=J;
   wezly(J,2,:)=IMAGE_SIZE*rand(1,1); 
   wezly(J,3,:)=IMAGE_SIZE*rand(1,1);
   
   while(wezly(J,2,:)< POINT_FREE_ZONE || ...
        wezly(J,3,:)< POINT_FREE_ZONE || ...
        wezly(J,2,:)> IMAGE_SIZE - POINT_FREE_ZONE || ...
        wezly(J,3,:)> IMAGE_SIZE - POINT_FREE_ZONE)
        
        wezly(J,2,:)= IMAGE_SIZE * rand(1); 
        wezly(J,3,:)= IMAGE_SIZE * rand(1); 
          
    end
      
end



%% Przetwarzamy obraz
Obraz=imread('JK.bmp'); %%Wczytanie obraz wykresu
SE = strel('octagon',9);
IMBI=imbinarize(Obraz); %%Binaryzacja
IMDIL=imdilate(~IMBI,SE); %%Dylatacja
IMDIL = imresize(IMDIL,[IMAGE_SIZE IMAGE_SIZE]); %%Zmiana rozmiaru
hold on
[kra]=wszystko(IMDIL); % T� funkcj� pobieramy wsp�rz�dne przeszk�d z wygenerowanego obrazu
%% Rysujemy przeszkody
for(i=1:size(kra,1))
    K=kra{i,1};
plot(K(:,2),K(:,1));
end
 %% Sprawdzamy, czy punkt ko�cowy lub pocz�tkowy znajduj� si� w przeszkodzie, je�li tak to funkcja jest przerywana i oddaje nam jedynk� w zmiennej �cie�ki
Inkre_3=1; %Zmienna inkrementacyjna
while(Inkre_3<size(kra,1)+1)
K=kra{Inkre_3,1};
in = inpolygon(wezly(1,2),wezly(1,3),K(:,2),K(:,1));  
Inkre_3=Inkre_3+1;
if(in==1)
    droga=1;
    return;
end
end
 Inkre_3=1;
 while(Inkre_3<size(kra,1)+1)
    K=kra{Inkre_3,1};
in = inpolygon(wezly(50,2),wezly(50,3),K(:,2),K(:,1)); 
Inkre_3=Inkre_3+1;
if(in==1)
    droga=1;
    return;
end
 end
 %% Sprawdzamy, kt�re proste mi�dzy w�z�ami przecinaj� si� z przeszkodami
 Dopusz=0; % Zmienna do sprawdzania, czy nowo utworzony w�ze� jest dopuszczalny (1 jest niedopuszczalny, 0 jest dopuszczalny)
for i=1:size(wezly,1)
   while(Inkre_2<size(kra,1)+1)
    K=kra{Inkre_2,1};
    in = inpolygon(wezly(i,2),wezly(i,3),K(:,2),K(:,1));  

    if(in==1) %Je�li przecina, to losujemy nowy punkt i sprawdzamy czy nie jest zawarty w niedopuszczalnym obszarze
             
    wezly(i,2)= IMAGE_SIZE * rand(1,1);
    wezly(i,3)= IMAGE_SIZE *rand(1,1);
    while(wezly(i,2,:)< POINT_FREE_ZONE || ...
          wezly(i,3,:)< POINT_FREE_ZONE || ...
          wezly(i,2,:)> IMAGE_SIZE - POINT_FREE_ZONE || ...
          wezly(i,3,:)> IMAGE_SIZE - POINT_FREE_ZONE)
       
         wezly(i,2)= IMAGE_SIZE * rand(1); 
         wezly(i,3)= IMAGE_SIZE * rand(1); 
    end
    Dopusz=1;
    Inkre_2=1;
    end
    if(Dopusz==0) %Je�li zmienna jest r�wna 0 sprawdzamy kolejny w�ze�
    Inkre_2=Inkre_2+1;
    end
    Dopusz=0;
    end
      
plot(wezly(i,2),wezly(i,3),'bo','LineWidth',10);
Inkre_2=1;

end
%% Tworzymy list� pokazuj�c�, kt�re w�z�y maj� do siebie dost�p
ZAJETE=0; % Zmienna informuj�ca, czy linia mi�dzy jednym w�z�em a drugim nie przechodzi przez przeszkod�(1 je�li przechodzi, 0 je�li nie)
for i=1:size(wezly,1)

   for y=1:size(wezly,1)
        
       for N=1:size(kra,1)
             K=kra{N,1};
             [x_przeciecia,y_przeciecia] = polyxpoly([wezly(i,2),wezly(y,2)],[wezly(i,3),wezly(y,3)],K(:,2),K(:,1));
       if numel(x_przeciecia) > 1     
          ZAJETE=1; % Je�li prosta si� przecina zamieniamy zmienn� "ZAJETE" na 1 i nie dodajemy tej pary do listy
       end
       end
       if ZAJETE==1
       
       else 
   plot([wezly(i,2),wezly(y,2)],[wezly(i,3),wezly(y,3)],'k');
   X_We(Inkre_1)=wezly(i,1);
   Y_We(Inkre_1)=wezly(y,1);
   Inkre_1=Inkre_1+1;
       end
       ZAJETE=0;
   end
end

segments = [(1:size(X_We,2)); Y_We; X_We]'; %Tworzymy macierz z po��czeniami w�z��w, kt�ra zostanie u�ywa w algorytmie

[~,Punkty_sciezki] = dijkstra(wezly, segments, 1, 50); % Puszczamy algorytm dijkstry

close all
%% Wype�niamy macierze warto�ciami otrzymanymi z algorytmu
    for(P_drogi=1:size(Punkty_sciezki,2))
       Punkt=[wezly(Punkty_sciezki(P_drogi),2) wezly(Punkty_sciezki(P_drogi),3)]; 
       % Tworzymy macierz �cie�ki dla Pure Pursuita(transfomujemy)
       droga(P_drogi,1)=Punkt(2);
       droga(P_drogi,2)=1900-Punkt(1);
       % Tworzymy macierz �cie�ki dla wizualizacji
       droga_wiz(P_drogi,1)=POIP(1);
       droga_wiz(P_drogi,2)=POIP(2);
       
    end
%% Przygotowujemy kontroler
controller = robotics.PurePursuit;
controller.Waypoints = droga;
controller.DesiredLinearVelocity =25;
controller.MaxAngularVelocity =10;
controller.LookaheadDistance =0.4;

