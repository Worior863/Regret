% Transformujemy uk�ad sto�u na uk�ad PP
Y1=makehgtform('translate',[1900 1900 0],'zrotate',-pi/2,'xrotate',pi); 
% Tworzymy uk�ad robota na podstawie uk�adu sto�u i robimy z niego uk�ad
% prawoskr�tny
Y3=makehgtform('translate',[TRU1 TRU2 0],'zrotate',TRU3,'xrotate',pi);
% Transformujemy uk�ad robota do uk�adu PP
OI=Y1*Y3;
% Wyci�gamy wsp�rz�dne X i Y
TRU1=OI(1,4);
TRU2=OI(2,4);
%% Wyliczamy orientacj�
X=OI(2,1)*1;
Y=OI(1,1)*1;
Omega=atan2(X,Y); % T� warto�� b�dziemy wrzuca� do funkcji controller by zdoby� pr�dko�ci robota
%% Dodatkowo je�li by�my chcieli zna� k�t w stopniach
Omega1=atan2d(X,Y);
if(Omega1<0)
   Omega1=Omega1+360;  
end
